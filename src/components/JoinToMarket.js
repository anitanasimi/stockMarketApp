import React, { useState } from 'react'

export default function JoinMarket({ joinMarket }) {

    const [username, setUsername] = useState();

    const handleSubmit = (e) => {
        e.preventDefault();
        if (username) {
            joinMarket(username);
        }
    }

    return (
        <div className="d-flex align-items-center justify-content-center vh-100">
            <div className="card w-50">
                <div className="card-body">
                    <h2 className="text-center mb-4">Join to Market</h2>
                    <form onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label htmlFor="username" className="form-label">Username</label>
                            <input type="text" className="form-control" id="username" onChange={e => setUsername(e.target.value)} required />
                        </div>
                        <button type="submit" disabled={!username || username === ''} className="btn btn-primary w-100">Join Market</button>
                    </form>
                </div>
            </div>
        </div>
    )
}
