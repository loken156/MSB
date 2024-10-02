// services/adminService.js

const API_BASE_URL = 'https://localhost:5001/api';

export const createAdmin = async (adminData) => {
  const response = await fetch(`${API_BASE_URL}/AdminMaker/create-admin`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
    },
    body: JSON.stringify(adminData),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Failed to create admin: ${errorText}`);
  }

  return await response.json();
};
