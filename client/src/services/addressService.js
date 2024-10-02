// services/addressService.js

const API_BASE_URL = 'https://localhost:5001/api';

export const getAllAddresses = async () => {
  const token = localStorage.getItem('token');
  if (!token) throw new Error('User not authenticated');

  const response = await fetch(`${API_BASE_URL}/Address/GetAllAddresses`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error(`Error fetching addresses: ${response.status}`);
  }

  return await response.json();
};

export const getAddressById = async (addressId) => {
  const response = await fetch(`${API_BASE_URL}/Address/${addressId}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
    },
  });

  if (!response.ok) {
    throw new Error(`Error fetching address by ID: ${response.status}`);
  }

  return await response.json();
};

export const addAddress = async (addressData) => {
  const response = await fetch(`${API_BASE_URL}/Address/AddAddress`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
    },
    body: JSON.stringify({ NewAddress: addressData }),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error adding address: ${errorText}`);
  }

  return await response.json();
};

export const updateAddress = async (addressId, addressData) => {
  const response = await fetch(`${API_BASE_URL}/Address/${addressId}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
    },
    body: JSON.stringify({ addressId, ...addressData }),
  });

  // Проверяем, является ли ответ JSON
  const isJson = response.headers.get('content-type')?.includes('application/json');
  const data = isJson ? await response.json() : await response.text();

  if (!response.ok) {
    throw new Error(`Error updating address: ${data}`);
  }

  return data; // Возвращаем строку "Update successful" или JSON-ответ
};


export const deleteAddress = async (addressId) => {
  const response = await fetch(`${API_BASE_URL}/Address/${addressId}`, {
    method: 'DELETE',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('token')}`,
    },
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error deleting address: ${errorText}`);
  }

  return 'Address deleted successfully';
};
