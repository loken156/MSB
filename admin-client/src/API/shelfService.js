
const API_BASE_URL = 'https://localhost:5001/api';

const getAuthHeaders = () => {
  const token = localStorage.getItem('token');
  if (!token) throw new Error('User not authenticated');

  return {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`,
  };
};

// Add new shelf
export const addShelf = async (shelfData) => {
  const requestBody = {
    newShelf: {
      shelfId: shelfData.shelfId || "00000000-0000-0000-0000-000000000000", // Генерация или использование уже существующего shelfId
      section: shelfData.section,
      shelfRows: shelfData.shelfRows,
      shelfColumn: shelfData.shelfColumn,
      largeBoxCapacity: shelfData.largeBoxCapacity,
      mediumBoxCapacity: shelfData.mediumBoxCapacity,
      smallBoxCapacity: shelfData.smallBoxCapacity,
      occupancy: shelfData.occupancy,
      warehouseName: shelfData.warehouseName
    },
    warehouseName: shelfData.warehouseName // Warehouse name is also provided
  };

  const response = await fetch(`${API_BASE_URL}/Shelf/AddShelf`, {
    method: 'POST',
    headers: getAuthHeaders(),
    body: JSON.stringify(requestBody)
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error adding shelf: ${errorText}`);
  }

  return await response.json();
};

// Get all shelves
export const getAllShelves = async () => {
  const response = await fetch(`${API_BASE_URL}/Shelf/GetAllShelves`, {
    method: 'GET',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error fetching shelves: ${errorText}`);
  }

  return await response.json();  // Убедитесь, что сервер возвращает все необходимые данные для полок
};

// Get shelf by ID
export const getShelfById = async (shelfId) => {
  const response = await fetch(`${API_BASE_URL}/Shelf/GetShelfBy/${shelfId}`, {
    method: 'GET',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error fetching shelf by ID: ${errorText}`);
  }

  return await response.json();
};

// Update shelf
export const updateShelf = async (shelfId, shelfData) => {
  const requestBody = {
    // Убедимся, что данные находятся внутри объекта shelfDto
    shelfDto: {
      shelfId: shelfId,
      shelfRow: parseInt(shelfData.shelfRow, 10),
      shelfColumn: parseInt(shelfData.shelfColumn, 10),
      occupancy: shelfData.occupancy,
      warehouseName: shelfData.warehouseName, // Убедимся, что передаем это поле
      warehouseId: shelfData.warehouseId || "00000000-0000-0000-0000-000000000000" // Убедимся, что warehouseId является валидным GUID
    }
  };

  const response = await fetch(`${API_BASE_URL}/Shelf/UpdateShelfBy/${shelfId}`, {
    method: 'PUT',
    headers: getAuthHeaders(),
    body: JSON.stringify(requestBody),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error updating shelf: ${errorText}`);
  }

  return await response.json();
};


// Delete shelf by ID
export const deleteShelf = async (shelfId) => {
  const response = await fetch(`${API_BASE_URL}/Shelf/DeleteShelfBy/${shelfId}`, {
    method: 'DELETE',
    headers: getAuthHeaders(),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(`Error deleting shelf: ${errorText}`);
  }

  return 'Shelf deleted successfully';
};

