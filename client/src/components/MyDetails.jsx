// components/MyDetails.jsx
import React, { useEffect, useState } from 'react';
import { getUserById } from '../services/userService';
import { getUserIdFromToken } from '../utils/authUtils';

function MyDetails() {
  const [userDetails, setUserDetails] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      const userId = getUserIdFromToken(token); 
      if (userId) {
        getUserById(userId)
          .then((data) => {
            console.log('User data fetched:', data);
            setUserDetails(data);
          })
          .catch((error) => {
            console.error('Error fetching user details:', error);
          });
      } else {
        console.error('No userId found in token');
      }
    } else {
      console.error('No token found in localStorage');
    }
  }, []);

  if (!userDetails) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h2>My Details</h2>
      <p>First Name: {userDetails.firstName}</p>
      <p>Last Name: {userDetails.lastName}</p>
      <p>Username: {userDetails.username}</p>
      <p>Email: {userDetails.email}</p>
      <p>Phone Number: {userDetails.phoneNumber}</p>
    </div>
  );
}

export default MyDetails;
