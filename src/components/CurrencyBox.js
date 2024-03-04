const CurrencyBox = ({ currency, onBuyClick, onSellClick }) => {
    return (
        <div className="rounded currency-box p-3 mb-3" style={{ backgroundColor: '#f8f9fa' }}>
            <h3>{currency.title}</h3>
            <button className="btn btn-success mr-2" onClick={() => onBuyClick(currency)}>Buy</button>
            <button className="btn btn-danger" onClick={() => onSellClick(currency)}>Sell</button>
        </div>
    );
};

export default CurrencyBox;