// services/warehouseService.js

const API_BASE_URL = 'https://localhost:5001/api';

const getAuthHeaders = () => {
  const token = localStorage.getItem('token');
  if (!token) throw new Error('User not authenticated');

  return {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`,
  };
};

// Get all warehouses
export const getAllWarehouses = async () => {
  const response = await fetch(`${API_BASE_URL}/Warehouse/GetAllWareHouses`, {
    method: 'GET',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error fetching warehouses: ${errorText}`);
  }

  return await response.json();
};

// Get a warehouse by ID
export const getWarehouseById = async (id) => {
  const response = await fetch(`${API_BASE_URL}/Warehouse/GetWarehouseBy${id}`, {
    method: 'GET',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error fetching warehouse by ID: ${errorText}`);
  }

  return await response.json();
};

// Add a new warehouse
export const addWarehouse = async (warehouseData) => {
  const response = await fetch(`${API_BASE_URL}/Warehouse/AddWarehouse`, {
    method: 'POST',
    headers: getAuthHeaders(),
    body: JSON.stringify({ newWarehouse: warehouseData }), // Adjusted to match your Swagger example
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error adding warehouse: ${errorText}`);
  }

  return await response.json();
};

// Update a warehouse
export const updateWarehouse = async (warehouseData) => {
  const response = await fetch(`${API_BASE_URL}/Warehouse/UpdateWarehouse`, {
    method: 'PUT',
    headers: getAuthHeaders(),
    body: JSON.stringify({ warehouse: warehouseData }), // Adjusted to match your Swagger example
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error updating warehouse: ${errorText}`);
  }

  return await response.json();
};

// Delete a warehouse by ID
export const deleteWarehouse = async (id) => {
  const response = await fetch(`${API_BASE_URL}/Warehouse/DeleteWarehouseby${id}`, {
    method: 'DELETE',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error deleting warehouse: ${errorText}`);
  }

  return 'Warehouse deleted successfully';
};
