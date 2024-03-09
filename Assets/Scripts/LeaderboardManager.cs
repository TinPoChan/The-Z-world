using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    // Create a leaderboard with this ID in the Unity Cloud Dashboard
    const string LeaderboardId = "The_Z-World_Leaderboard";

    [SerializeField] private TMP_Text leaderboardText; // Assign in Inspector
    [SerializeField] private TMP_InputField playerNameInput; // Optional, assign if used
    [SerializeField] private int playerScore; // Set this when the game is over
    [SerializeField] private TMP_Text rank1Name; // Assign this in the Inspector
    [SerializeField] private TMP_Text rank1Score; // Assign this in the Inspector
    // Generate rank 2 - 5 name and score fields here
    [SerializeField] private TMP_Text rank2Name; // Assign this in the Inspector
    [SerializeField] private TMP_Text rank2Score; // Assign this in the Inspector
    [SerializeField] private TMP_Text rank3Name; // Assign this in the Inspector
    [SerializeField] private TMP_Text rank3Score; // Assign this in the Inspector
    [SerializeField] private TMP_Text rank4Name; // Assign this in the Inspector
    [SerializeField] private TMP_Text rank4Score; // Assign this in the Inspector
    [SerializeField] private TMP_Text rank5Name; // Assign this in the Inspector
    [SerializeField] private TMP_Text rank5Score; // Assign this in the Inspector

    [SerializeField] private TMP_Text yourRank; // Assign this in the Inspector
    [SerializeField] private TMP_Text yourName; // Assign this in the Inspector
    [SerializeField] private TMP_Text yourScore; // Assign this in the Inspector

    string VersionId { get; set; }
    int Offset { get; set; }
    int Limit { get; set; }
    int RangeLimit { get; set; }
    List<string> FriendIds { get; set; }

    async void Awake()
    {
        await UnityServices.InitializeAsync();

        await SignInAnonymously();

        UpdateTopRanks();
    }

    public async Task SignInAnonymously()
    {
        // Check if the player is already signed in
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            AuthenticationService.Instance.SignedIn += () =>
            {
                Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
            };
            AuthenticationService.Instance.SignInFailed += s =>
            {
                // Take some action here...
                Debug.Log(s);
            };

            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to sign in anonymously: {e.Message}");
            }
        }
        else
        {
            Debug.Log("Player is already signed in.");
        }
    }
    public string TruncateString(string input, int maxLength)
    {
        if (string.IsNullOrEmpty(input)) return input;
        if (input.Length <= maxLength) return input;

        // Truncate string and append "..."
        return input.Substring(0, maxLength) + "...";
    }

    public async void UpdateRank1Name()
    {
        try
        {
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Limit = 1 });
            //Debug.Log(JsonConvert.SerializeObject(scoresResponse.Results.Count));

            if (scoresResponse.Results.Count > 0)
            {
                //// Assuming the player ID or another identifier is used as the player's name
                //rank1Name.text = scoresResponse.Results[0].PlayerId; // Or any other field representing the player's name
                string truncatedName = TruncateString(scoresResponse.Results[0].PlayerId, 7);
                rank1Name.text = truncatedName;
                rank1Score.text = scoresResponse.Results[0].Score.ToString();
            }
            else
            {
                rank1Name.text = "N/A";
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to fetch leaderboard scores: {e.Message}");
            rank1Name.text = "Error";
        }
    }
    public async void UpdateTopRanks()
    {
        try
        {
            // Fetch the top 5 scores from the leaderboard
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Limit = 5 });

            // Check if there are at least 5 scores to display
            if (scoresResponse.Results.Count > 0)
            {
                // Update rank 1 - 5 names and scores
                for (int i = 0; i < scoresResponse.Results.Count; i++)
                {
                    var scoreEntry = scoresResponse.Results[i];
                    string truncatedName = TruncateString(scoreEntry.PlayerName, 7);

                    switch (i)
                    {
                        case 0:
                            rank1Name.text = truncatedName;
                            rank1Score.text = scoreEntry.Score.ToString();
                            break;
                        case 1:
                            rank2Name.text = truncatedName;
                            rank2Score.text = scoreEntry.Score.ToString();
                            break;
                        case 2:
                            rank3Name.text = truncatedName;
                            rank3Score.text = scoreEntry.Score.ToString();
                            break;
                        case 3:
                            rank4Name.text = truncatedName;
                            rank4Score.text = scoreEntry.Score.ToString();
                            break;
                        case 4:
                            rank5Name.text = truncatedName;
                            rank5Score.text = scoreEntry.Score.ToString();
                            break;
                    }
                }
            }
            else
            {
                // Handle the case where there are fewer than 5 scores
                rank1Name.text = "N/A";
                rank1Score.text = "";
                // ... Set the rest of the ranks to "N/A" or blank as well
                rank2Name.text = "N/A";
                rank2Score.text = "";
                rank3Name.text = "N/A";
                rank3Score.text = "";
                rank4Name.text = "N/A";
                rank4Score.text = "";
                rank5Name.text = "N/A";
                rank5Score.text = "";
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to fetch leaderboard scores: {e.Message}");
            // ... Set all ranks to "Error" or an appropriate message
        }
    }


    public async void AddScore()
    {
        // Optional: Get player name from input field
        string playerName = playerNameInput != null ? playerNameInput.text : "Anonymous";
        

        // Add the score
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, playerScore);
        Debug.Log($"Score added for {playerName}: {JsonConvert.SerializeObject(scoreResponse)}");

        //update the player's name
        var updateResponse = await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
        Debug.Log($"Player name updated: {JsonConvert.SerializeObject(updateResponse)}");

        // Refresh leaderboard to show the latest scores
        GetScores();

        UpdateTopRanks();

        // Update the UI to show the player's name and score and rank
        yourName.text = playerName;
        yourScore.text = playerScore.ToString();
        yourRank.text = "Your Rank is Rank: " + scoreResponse.Rank + 1;

    }


    public async void GetScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);

        // Format and display the scores on your UI
        //string leaderboardString = "Leaderboard:\n";
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
        
    }

    public void setPlayerScore(int score)
    {
        playerScore = score;
    }

    public async void GetPaginatedScores()
    {
        Offset = 10;
        Limit = 10;
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = Offset, Limit = Limit });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    public async void GetPlayerScore()
    {
        var scoreResponse =
            await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async void GetVersionScores()
    {
        var versionScoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
        Debug.Log(JsonConvert.SerializeObject(versionScoresResponse));
    }
}
