import React, { useState, useEffect } from 'react';
import CurrencyBox from './CurrencyBox';
import MarketMembers from './MarketMembers';
import TransactionItems from './TransactionItems';

const Market = ({ messages, tMessage,tMessageStatics, createTransaction }) => {
    const [currencies, setCurrencies] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchCurrencies = async () => {
            try {

                const serverURL = process.env.ServerAddress ?? "https://localhost:44373/";

                const response = await fetch(serverURL + 'api/Currency');
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                setCurrencies(data);
                setLoading(false);
            } catch (err) {
                setError('Error fetching currencies. Please try again later.');
                setLoading(false);
            }
        };

        fetchCurrencies();
    }, []);

    const handleBuyClick = (currency) => {
        const transactionDto = {
            CurrencyId: currency.id,
            Count: 1,
            OperationType: 2,
        };

        console.log(transactionDto)
        createTransaction(transactionDto);
    };

    const handleSellClick = (currency) => {
        const transactionDto = {
            CurrencyId: currency.id,
            Count: 1,
            OperationType: 1,
        };
        console.log(transactionDto)
        createTransaction(transactionDto);
    };

    return (

        <div>
            <div style={{ margin: '10px' }}>
                <MarketMembers messages={messages} />
            </div>
            <div style={{ margin: '10px' }}>
                <TransactionItems tmessage={tMessage} tmessageStatics={tMessageStatics} />
            </div>
            <div style={{ margin: '10px' }}>
                {loading ? (
                    <p>Loading currencies...</p>
                ) : error ? (
                    <p>{error}</p>
                ) : (
                    <div className="currency-list">
                        {currencies.map((currency) => (
                            <CurrencyBox
                                key={currency.id}
                                currency={currency}
                                onBuyClick={handleBuyClick}
                                onSellClick={handleSellClick}
                            />
                        ))}
                    </div>
                )}
            </div>
        </div>

    );
};

export default Market;
