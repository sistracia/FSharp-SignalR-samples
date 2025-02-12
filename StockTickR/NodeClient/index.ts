import * as signalR from "@microsoft/signalr";

const hubConnection = new signalR.HubConnectionBuilder()
  .withUrl("http://127.0.0.1:5050/stocks")
  .build();

hubConnection.start().then(() => {
  hubConnection.stream("StreamStocks").subscribe({
    next: (stock) => {
      console.log(stock);
      // console.log(stock.Symbol + " " + stock.Price);
    },
    error: (err) => {},
    complete: () => {},
  });
});
