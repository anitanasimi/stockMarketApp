import React from 'react';
import Alert from 'react-bootstrap/Alert';

const TransactionItems = ({ tmessage, tmessageStatics }) => (
  <div className="message-box-transaction">
    {tmessage && tmessageStatics && tmessage.length > 0 && tmessageStatics.length > 0 ? (
      <>
        {tmessage.map((msg, index) => (
          <Alert key={index} variant="info">{msg}</Alert>
        ))}
        {tmessageStatics.map((msg, index) => (
          <Alert key={index} variant="warning">{msg}</Alert>
        ))}
      </>
    ) : (
      <Alert variant="info">No transactions available.</Alert>
    )}
  </div>
);

export default TransactionItems;
