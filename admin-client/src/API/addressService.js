// services/addressService.js

const API_BASE_URL = 'https://localhost:5001/api';

const getAuthHeaders = () => {
  const token = localStorage.getItem('token');
  if (!token) throw new Error('User not authenticated');

  return {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`,
  };
};

// Get address by ID
export const getAddressById = async (addressId) => {
  const response = await fetch(`${API_BASE_URL}/Address/${addressId}`, {
    method: 'GET',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error fetching address: ${errorText}`);
  }

  return await response.json();
};
