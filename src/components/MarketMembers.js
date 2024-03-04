import React from 'react';
import ListGroup from 'react-bootstrap/ListGroup';

const MarketMembers = ({ messages }) => (
  <div className="message-box">
    <ListGroup>
      {messages.map((messageObj, index) => (
        <ListGroup.Item key={index} className="message">
          <p>{messageObj.username}:</p>
          <strong>{messageObj.message}</strong>
          <hr/>
          <small>{new Date(messageObj.date).toLocaleString()}</small>
        </ListGroup.Item>
      ))}
    </ListGroup>
  </div>
);

export default MarketMembers;
