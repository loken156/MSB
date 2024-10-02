// services/userService.js

const API_BASE_URL = 'https://localhost:5001/api';

const getAuthHeaders = () => {
  const token = localStorage.getItem('token');
  if (!token) throw new Error('User not authenticated');

  return {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`,
  };
};

// Get all users
export const getAllUsers = async () => {
  const response = await fetch(`${API_BASE_URL}/User/Getallusers`, {
    method: 'GET',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error fetching users: ${errorText}`);
  }

  return await response.json();
};

// Get user by ID
export const getUserById = async (id) => {
  const response = await fetch(`${API_BASE_URL}/User/GetUserbyId?UserId=${encodeURIComponent(id)}`, {
    method: 'GET',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error fetching user by ID: ${errorText}`);
  }

  return await response.json();
};

// Update user
export const updateUser = async (userId, userData) => {
  const response = await fetch(`${API_BASE_URL}/User/UpdateUser?updatedUserId=${encodeURIComponent(userId)}`, {
    method: 'PUT',
    headers: getAuthHeaders(),
    body: JSON.stringify(userData), // Assumes `userData` is in the format required by the API
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error updating user: ${errorText}`);
  }

  return await response.json();
};

// Delete user by ID
export const deleteUser = async (id) => {
  const response = await fetch(`${API_BASE_URL}/User/DeleteUserby${encodeURIComponent(id)}`, {
    method: 'DELETE',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error deleting user: ${errorText}`);
  }

  return 'User deleted successfully';
};

// Change password
export const changePassword = async (userId, currentPassword, newPassword) => {
  const response = await fetch(
    `${API_BASE_URL}/User/ChangePassword?userId=${encodeURIComponent(userId)}&currentPassword=${encodeURIComponent(currentPassword)}&newPassword=${encodeURIComponent(newPassword)}`,
    {
      method: 'POST',
      headers: getAuthHeaders(),
    }
  );

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error changing password: ${errorText}`);
  }

  return await response.text();
};
