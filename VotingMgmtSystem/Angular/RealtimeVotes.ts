import * as signalR from "@microsoft/signalr";

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/pollHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

hubConnection.on("ReceiveUpdate", (voteCounts) => {
    // Handle the real-time update of vote counts
    // Update your UI or take any necessary actions
});

hubConnection.start().then(() => {
    // Join the specific poll group when the connection is established
    hubConnection.invoke("JoinPollGroup", pollId).catch((err) => console.error(err));
});