import React, { useState } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import JoinToMarket from './components/JoinToMarket';
import { HubConnectionBuilder, LogLevel , HubConnectionState} from '@microsoft/signalr';
import Market from './components/Market';

function App() {

  const [connection, setConnection] = useState();
  const [messages, setMessages] = useState([]);
  const [tMessage, setTMessage] = useState([]);
  const [tMessageStatics, settMessageStatics] = useState([]);

  const createTransaction = async (transaction) => {
    try {
      const hubUrl = process.env.HubAddress || "https://localhost:44373/market-hub";
      let newConnection = connection;
  
      if (!connection || connection.state !== HubConnectionState.Connected) {
        newConnection = new HubConnectionBuilder()
          .withUrl(hubUrl)
          .configureLogging(LogLevel.Information)
          .build();
      }
  
      newConnection.on("ReceiveTransaction", (message, messageStatics, isSuccess) => {
        setTMessage(tMessage => [...tMessage, message]);
        settMessageStatics(tMessageStatics => [...tMessageStatics, messageStatics]);
  
        console.log('transaction:', isSuccess);
        console.log('t message:', message);
        console.log('t messsage statics:', messageStatics);
      });
  
      if (newConnection.state !== HubConnectionState.Connected) {
        await newConnection.start();
      }
      await newConnection.invoke("JoinTransactions", transaction);
  
      setConnection(newConnection);
    } catch (error) {
      console.log(error);
    }
  };
  
  const joinMarket = async (username) => {
    try {
      const hubUrl = process.env.HubAddress || "https://localhost:44373/market-hub";
      let newConnection = connection;
  
      if (!connection || connection.state !== HubConnectionState.Connected) {
        newConnection = new HubConnectionBuilder()
          .withUrl(hubUrl)
          .configureLogging(LogLevel.Information)
          .build();
      }
  
      newConnection.on("ReceiveMessage", (username, message, date) => {
        setMessages(messages => [...messages, { username, message, date }]);
        console.log('message received:', message);
      });
  
      if (newConnection.state !== HubConnectionState.Connected) {
        await newConnection.start();
      }
      await newConnection.invoke("JoinMarket", { username });
  
      setConnection(newConnection);
    } catch (error) {
      console.log(error);
    }
  };
  

  return (
    <div className="App">
      <div>
        {!connection
          ? <JoinToMarket joinMarket={joinMarket} />
          : <Market messages={messages} tMessage={tMessage} tMessageStatics={tMessageStatics} createTransaction={createTransaction} />
        }
      </div>
    </div>
  );
}

export default App;
