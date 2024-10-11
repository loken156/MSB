// src/pages/SuccessPage.js
import React, { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import "../css/SuccessPage.css"

const SuccessPage = () => {
    const location = useLocation();
    const queryParams = new URLSearchParams(location.search);
    const sessionId = queryParams.get('session_id');
    const [sessionData, setSessionData] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchSessionData = async () => {
            if (!sessionId) return;
            try {
                const response = await fetch(`https://localhost:5001/api/stripe/session/${sessionId}`);
                if (!response.ok) {
                    throw new Error('Failed to fetch session data');
                }
                const data = await response.json();
                console.log('Fetched session data:', data);
                setSessionData(data);
                setIsLoading(false);
            } catch (error) {
                console.error('Error fetching session data:', error);
                setError(error);
                setIsLoading(false);
            }
        };

        fetchSessionData();
    }, [sessionId]);

    return (
        <div className="container">
            <div className="info-container">
                <h1>Thank your for your purchase!</h1>
                <p>Your payment has been processed successfully.</p>
                {isLoading ? (
                    <p>Loading payment details...</p>
                ) : error 
                ? (
                    <p>Error loading payment details. Please try again.</p>
                ) : sessionData ? (
                    <div>
                        <p>A confirmation will be sent to: {sessionData.customerdetails.customerDetails.email || 'Not available'}</p>
                        <h3>Purchased Items:</h3>
                        {sessionData.items && sessionData.items.length > 0 ? (
                            <ul>
                                {sessionData.items.map((item, index) => (
                                    <li key={index}>
                                        <p>Description: {item.description}</p>
                                        <p>Quantity: {item.quantity}</p>
                                        <p>Subtotal: ${(item.amountSubtotal / 100).toFixed(2)} SGD</p>
                                        <p>Total: ${(item.amountTotal / 100).toFixed(2)} SGD</p>
                                    </li>
                                ))}
                            </ul>
                        ) : (
                            <p>No items found.</p>
                        )}
                    </div>
                ) : (
                    <p>No session data available.</p>
                )}
            </div>
        </div>
    );
    
};

export default SuccessPage;
