// components/ApiTestPage.jsx

import React, { useState } from 'react';
import '../css/ApiTestPage.css';
import { getAllAddresses, addAddress, updateAddress, deleteAddress, getAddressById } from '../services/addressService';
import { createAdmin } from '../services/adminService';
import { getAllWarehouses, getWarehouseById, addWarehouse, updateWarehouse, deleteWarehouse } from '../services/warehouseService';
import { getAllUsers, getUserById, updateUser, deleteUser, changePassword } from '../services/userService';
import { addShelf, getAllShelves, getShelfById, updateShelf, deleteShelf } from '../services/shelfService';


function ApiTestPage() {

  // adress js
  const [addressId, setAddressId] = useState('');
  const [addressData, setAddressData] = useState({
    streetName: '',
    streetNumber: '',
    apartment: '',
    zipCode: '',
    floor: '',
    city: '',
    state: '',
    country: '',
  });

  const handleGetAllAddresses = async () => {
    try {
      const data = await getAllAddresses();
      setResponse(data);
    } catch (error) {
      console.error('Error fetching addresses:', error.message);
      setResponse(error.message);
    }
  };

  const handleGetAddressById = async () => {
    try {
      const data = await getAddressById(addressId);
      setResponse(data);
    } catch (error) {
      console.error('Error fetching address by ID:', error.message);
      setResponse(error.message);
    }
  };

  const handleAddAddress = async () => {
    try {
      const data = await addAddress(addressData);
      setResponse(data);
    } catch (error) {
      console.error('Error adding address:', error.message);
      setResponse(error.message);
    }
  };

  const handleUpdateAddress = async () => {
    try {
      const data = await updateAddress(addressId, addressData);
      setResponse(data);
    } catch (error) {
      console.error('Error updating address:', error.message);
      setResponse(error.message);
    }
  };

  const handleDeleteAddress = async () => {
    try {
      const message = await deleteAddress(addressId);
      setResponse(message);
    } catch (error) {
      console.error('Error deleting address:', error.message);
      setResponse(error.message);
    }
  };

  // admin js

  const [adminData, setAdminData] = useState({
    email: '',
    password: '',
    firstName: '',
    lastName: '',
  });

  const handleCreateAdmin = async () => {
    try {
      const data = await createAdmin(adminData);
      setResponse(data);
    } catch (error) {
      console.error('Error creating admin:', error.message);
      setResponse(error.message);
    }
  };

  // warehouse js

  const [warehouseId, setWarehouseId] = useState('');
  const [warehouseData, setWarehouseData] = useState({
    warehouseName: '',
    addressId: '',
  });

  const handleGetAllWarehouses = async () => {
    try {
      const data = await getAllWarehouses();
      setResponse(data);
    } catch (error) {
      console.error('Error fetching warehouses:', error.message);
      setResponse(error.message);
    }
  };

  const handleGetWarehouseById = async () => {
    try {
      const data = await getWarehouseById(warehouseId);
      setResponse(data);
    } catch (error) {
      console.error('Error fetching warehouse by ID:', error.message);
      setResponse(error.message);
    }
  };

  const handleAddWarehouse = async () => {
    try {
      const data = await addWarehouse(warehouseData);
      setResponse(data);
    } catch (error) {
      console.error('Error adding warehouse:', error.message);
      setResponse(error.message);
    }
  };

  const handleUpdateWarehouse = async () => {
    try {
      const data = await updateWarehouse({ warehouseId, ...warehouseData });
      setResponse(data);
    } catch (error) {
      console.error('Error updating warehouse:', error.message);
      setResponse(error.message);
    }
  };

  const handleDeleteWarehouse = async () => {
    try {
      const message = await deleteWarehouse(warehouseId);
      setResponse(message);
    } catch (error) {
      console.error('Error deleting warehouse:', error.message);
      setResponse(error.message);
    }
  };

  // user js

  const [userId, setUserId] = useState('');
  const [userData, setUserData] = useState({
    email: '',
    firstName: '',
    lastName: '',
    phoneNumber: '',
    address: {
      streetName: '',
      streetNumber: '',
      apartment: '',
      zipCode: '',
      floor: '',
      city: '',
      state: '',
      country: '',
    },
  });

  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');

  const handleGetAllUsers = async () => {
    try {
      const data = await getAllUsers();
      setResponse(data);
    } catch (error) {
      console.error('Error fetching users:', error.message);
      setResponse(error.message);
    }
  };

  const handleGetUserById = async () => {
    try {
      const data = await getUserById(userId);
      setResponse(data);
    } catch (error) {
      console.error('Error fetching user by ID:', error.message);
      setResponse(error.message);
    }
  };

  const handleUpdateUser = async () => {
    try {
      const data = await updateUser(userId, userData);
      setResponse(data);
    } catch (error) {
      console.error('Error updating user:', error.message);
      setResponse(error.message);
    }
  };

  const handleDeleteUser = async () => {
    try {
      const message = await deleteUser(userId);
      setResponse(message);
    } catch (error) {
      console.error('Error deleting user:', error.message);
      setResponse(error.message);
    }
  };

  const handleChangePassword = async () => {
    try {
      const data = await changePassword(userId, currentPassword, newPassword);
      setResponse(data);
    } catch (error) {
      console.error('Error changing password:', error.message);
      setResponse(error.message);
    }
  };

  // shelf js

  const [shelfId, setShelfId] = useState('');
  const [shelfData, setShelfData] = useState({
    shelfId: '',  // Добавляем shelfId
    shelfRow: '',
    shelfColumn: '',
    occupancy: false,
    warehouseName: '',
    warehouseId: '', // Добавляем warehouseId для корректного запроса
  });

  const handleAddShelf = async () => {
    try {
      const newShelfData = {
        shelfId: shelfData.shelfId || null, // Используем null, если нет ID
        shelfRow: parseInt(shelfData.shelfRow, 10),
        shelfColumn: parseInt(shelfData.shelfColumn, 10),
        occupancy: shelfData.occupancy,
        warehouseName: shelfData.warehouseName,
        warehouseId: shelfData.warehouseId,
      };

      const data = await addShelf(newShelfData);
      setResponse(data);
    } catch (error) {
      console.error('Error adding shelf:', error.message);
      setResponse(error.message);
    }
  };

  const handleGetAllShelves = async () => {
    try {
      const data = await getAllShelves();
      setResponse(data);
    } catch (error) {
      console.error('Error fetching shelves:', error.message);
      setResponse(error.message);
    }
  };

  const handleGetShelfById = async () => {
    try {
      const data = await getShelfById(shelfId);
      setResponse(data);
    } catch (error) {
      console.error('Error fetching shelf by ID:', error.message);
      setResponse(error.message);
    }
  };

  const handleUpdateShelf = async () => {
    try {
      const updatedShelfData = {
        shelfId: shelfId,
        shelfRow: parseInt(shelfData.shelfRow, 10),
        shelfColumn: parseInt(shelfData.shelfColumn, 10),
        occupancy: shelfData.occupancy,
        warehouseName: shelfData.warehouseName,
        warehouseId: shelfData.warehouseId || "00000000-0000-0000-0000-000000000000" // Добавляем warehouseId, если его нет
      };

      const data = await updateShelf(shelfId, updatedShelfData);
      setResponse(data);
    } catch (error) {
      console.error('Error updating shelf:', error.message);
      setResponse(error.message);
    }
  };

  
  const handleDeleteShelf = async () => {
    try {
      const message = await deleteShelf(shelfId);
      setResponse(message);
    } catch (error) {
      console.error('Error deleting shelf:', error.message);
      setResponse(error.message);
    }
  };

  


  const [response, setResponse] = useState(null);

  return (
    <div className="apiTestContainer">
      <h1 className="heading">Address API</h1>
      <div>
        <h2>Add Address</h2>
        <input
          type="text"
          placeholder="Street Name"
          value={addressData.streetName}
          onChange={(e) => setAddressData({ ...addressData, streetName: e.target.value })}
        />
        <input
          type="text"
          placeholder="Street Number"
          value={addressData.streetNumber}
          onChange={(e) => setAddressData({ ...addressData, streetNumber: e.target.value })}
        />
        <input
          type="text"
          placeholder="Apartment"
          value={addressData.apartment}
          onChange={(e) => setAddressData({ ...addressData, apartment: e.target.value })}
        />
        <input
          type="text"
          placeholder="Zip Code"
          value={addressData.zipCode}
          onChange={(e) => setAddressData({ ...addressData, zipCode: e.target.value })}
        />
        <input
          type="text"
          placeholder="Floor"
          value={addressData.floor}
          onChange={(e) => setAddressData({ ...addressData, floor: e.target.value })}
        />
        <input
          type="text"
          placeholder="City"
          value={addressData.city}
          onChange={(e) => setAddressData({ ...addressData, city: e.target.value })}
        />
        <input
          type="text"
          placeholder="State"
          value={addressData.state}
          onChange={(e) => setAddressData({ ...addressData, state: e.target.value })}
        />
        <input
          type="text"
          placeholder="Country"
          value={addressData.country}
          onChange={(e) => setAddressData({ ...addressData, country: e.target.value })}
        />
        <button onClick={handleAddAddress}>Add Address</button>
      </div>

      <div>
        <h2>Get All Addresses</h2>
        <button onClick={handleGetAllAddresses}>Get All Addresses</button>
      </div>

      <div>
        <h2>Get Address by ID</h2>
        <input
          type="text"
          placeholder="Address ID"
          value={addressId}
          onChange={(e) => setAddressId(e.target.value)}
        />
        <button onClick={handleGetAddressById}>Get Address by ID</button>
      </div>

      <div>
        <h2>Update Address</h2>
        <input
          type="text"
          placeholder="Address ID"
          value={addressId}
          onChange={(e) => setAddressId(e.target.value)}
        />
        <input
          type="text"
          placeholder="Street Name"
          value={addressData.streetName}
          onChange={(e) => setAddressData({ ...addressData, streetName: e.target.value })}
        />
        <input
          type="text"
          placeholder="Street Number"
          value={addressData.streetNumber}
          onChange={(e) => setAddressData({ ...addressData, streetNumber: e.target.value })}
        />
        <input
          type="text"
          placeholder="Apartment"
          value={addressData.apartment}
          onChange={(e) => setAddressData({ ...addressData, apartment: e.target.value })}
        />
        <input
          type="text"
          placeholder="Zip Code"
          value={addressData.zipCode}
          onChange={(e) => setAddressData({ ...addressData, zipCode: e.target.value })}
        />
        <input
          type="text"
          placeholder="Floor"
          value={addressData.floor}
          onChange={(e) => setAddressData({ ...addressData, floor: e.target.value })}
        />
        <input
          type="text"
          placeholder="City"
          value={addressData.city}
          onChange={(e) => setAddressData({ ...addressData, city: e.target.value })}
        />
        <input
          type="text"
          placeholder="State"
          value={addressData.state}
          onChange={(e) => setAddressData({ ...addressData, state: e.target.value })}
        />
        <input
          type="text"
          placeholder="Country"
          value={addressData.country}
          onChange={(e) => setAddressData({ ...addressData, country: e.target.value })}
        />
        <button onClick={handleUpdateAddress}>Update Address</button>
      </div>

      <div>
        <h2>Delete Address</h2>
        <input
          type="text"
          placeholder="Address ID"
          value={addressId}
          onChange={(e) => setAddressId(e.target.value)}
        />
        <button onClick={handleDeleteAddress}>Delete Address</button>
      </div>

      <h1 className="heading margin_top">Admin Maker API</h1>
      <div>
        <h2>Create Admin</h2>
        <input
          type="text"
          placeholder="Email"
          value={adminData.email}
          onChange={(e) => setAdminData({ ...adminData, email: e.target.value })}
        />
        <input
          type="password"
          placeholder="Password"
          value={adminData.password}
          onChange={(e) => setAdminData({ ...adminData, password: e.target.value })}
        />
        <input
          type="text"
          placeholder="First Name"
          value={adminData.firstName}
          onChange={(e) => setAdminData({ ...adminData, firstName: e.target.value })}
        />
        <input
          type="text"
          placeholder="Last Name"
          value={adminData.lastName}
          onChange={(e) => setAdminData({ ...adminData, lastName: e.target.value })}
        />
        <button onClick={handleCreateAdmin}>Create Admin</button>
      </div>

      <h1 className="heading margin_top">warehouse API</h1>
      <div>
        <h2>Add Warehouse</h2>
        <input
          type="text"
          placeholder="Warehouse Name"
          value={warehouseData.warehouseName}
          onChange={(e) => setWarehouseData({ ...warehouseData, warehouseName: e.target.value })}
        />
        <input
          type="text"
          placeholder="Address ID"
          value={warehouseData.addressId}
          onChange={(e) => setWarehouseData({ ...warehouseData, addressId: e.target.value })}
        />
        <button onClick={handleAddWarehouse}>Add Warehouse</button>
      </div>

      <div>
        <h2>Get All Warehouses</h2>
        <button onClick={handleGetAllWarehouses}>Get All Warehouses</button>
      </div>

      <div>
        <h2>Get Warehouse by ID</h2>
        <input
          type="text"
          placeholder="Warehouse ID"
          value={warehouseId}
          onChange={(e) => setWarehouseId(e.target.value)}
        />
        <button onClick={handleGetWarehouseById}>Get Warehouse by ID</button>
      </div>

      <div>
        <h2>Update Warehouse</h2>
        <input
          type="text"
          placeholder="Warehouse ID"
          value={warehouseId}
          onChange={(e) => setWarehouseId(e.target.value)}
        />
        <input
          type="text"
          placeholder="Warehouse Name"
          value={warehouseData.warehouseName}
          onChange={(e) => setWarehouseData({ ...warehouseData, warehouseName: e.target.value })}
        />
        <input
          type="text"
          placeholder="Address ID"
          value={warehouseData.addressId}
          onChange={(e) => setWarehouseData({ ...warehouseData, addressId: e.target.value })}
        />
        <button onClick={handleUpdateWarehouse}>Update Warehouse</button>
      </div>

      <div>
        <h2>Delete Warehouse</h2>
        <input
          type="text"
          placeholder="Warehouse ID"
          value={warehouseId}
          onChange={(e) => setWarehouseId(e.target.value)}
        />
        <button onClick={handleDeleteWarehouse}>Delete Warehouse</button>
      </div>

      <h1 className="heading margin_top">User API</h1>

      <div>
        <h2>Get All Users</h2>
        <button onClick={handleGetAllUsers}>Get All Users</button>
      </div>

      <div>
        <h2>Get User by ID</h2>
        <input
          type="text"
          placeholder="User ID"
          value={userId}
          onChange={(e) => setUserId(e.target.value)}
        />
        <button onClick={handleGetUserById}>Get User by ID</button>
      </div>

      <div>
        <h2>Update User</h2>
        <input
          type="text"
          placeholder="User ID"
          value={userId}
          onChange={(e) => setUserId(e.target.value)}
        />
        <input
          type="text"
          placeholder="Email"
          value={userData.email}
          onChange={(e) => setUserData({ ...userData, email: e.target.value })}
        />
        <input
          type="text"
          placeholder="First Name"
          value={userData.firstName}
          onChange={(e) => setUserData({ ...userData, firstName: e.target.value })}
        />
        <input
          type="text"
          placeholder="Last Name"
          value={userData.lastName}
          onChange={(e) => setUserData({ ...userData, lastName: e.target.value })}
        />
        <button onClick={handleUpdateUser}>Update User</button>
      </div>

      <div>
        <h2>Delete User</h2>
        <input
          type="text"
          placeholder="User ID"
          value={userId}
          onChange={(e) => setUserId(e.target.value)}
        />
        <button onClick={handleDeleteUser}>Delete User</button>
      </div>

      <div>
        <h2>Change Password</h2>
        <input
          type="text"
          placeholder="User ID"
          value={userId}
          onChange={(e) => setUserId(e.target.value)}
        />
        <input
          type="password"
          placeholder="Current Password"
          value={currentPassword}
          onChange={(e) => setCurrentPassword(e.target.value)}
        />
        <input
          type="password"
          placeholder="New Password"
          value={newPassword}
          onChange={(e) => setNewPassword(e.target.value)}
        />
        <button onClick={handleChangePassword}>Change Password</button>
      </div>

      <h1 className="heading margin_top">Shelf API</h1>

      <div>
        <h2>Add Shelf</h2>
        <input
          type="text"
          placeholder="Shelf Row"
          value={shelfData.shelfRow}
          onChange={(e) => setShelfData({ ...shelfData, shelfRow: e.target.value })}
        />
        <input
          type="text"
          placeholder="Shelf Column"
          value={shelfData.shelfColumn}
          onChange={(e) => setShelfData({ ...shelfData, shelfColumn: e.target.value })}
        />
        <label>
          <input
            type="checkbox"
            checked={shelfData.occupancy}
            onChange={(e) => setShelfData({ ...shelfData, occupancy: e.target.checked })}
          />
          Occupied
        </label>
        <input
          type="text"
          placeholder="Warehouse Name"
          value={shelfData.warehouseName}
          onChange={(e) => setShelfData({ ...shelfData, warehouseName: e.target.value })}
        />
        <button onClick={handleAddShelf}>Add Shelf</button>
      </div>

      <div>
        <h2>Get All Shelves</h2>
        <button onClick={handleGetAllShelves}>Get All Shelves</button>
      </div>

      <div>
        <h2>Get Shelf by ID</h2>
        <input
          type="text"
          placeholder="Shelf ID"
          value={shelfId}
          onChange={(e) => setShelfId(e.target.value)}
        />
        <button onClick={handleGetShelfById}>Get Shelf by ID</button>
      </div>

      <div>
        <h2>Update Shelf</h2>
        <input
          type="text"
          placeholder="Shelf ID"
          value={shelfId}
          onChange={(e) => setShelfId(e.target.value)}
        />
        <input
          type="text"
          placeholder="Shelf Row"
          value={shelfData.shelfRow}
          onChange={(e) => setShelfData({ ...shelfData, shelfRow: e.target.value })}
        />
        <input
          type="text"
          placeholder="Shelf Column"
          value={shelfData.shelfColumn}
          onChange={(e) => setShelfData({ ...shelfData, shelfColumn: e.target.value })}
        />
        <input
          type="checkbox"
          checked={shelfData.occupancy}
          onChange={(e) => setShelfData({ ...shelfData, occupancy: e.target.checked })}
        />
        Occupied
        <input
          type="text"
          placeholder="Warehouse Name"
          value={shelfData.warehouseName}
          onChange={(e) => setShelfData({ ...shelfData, warehouseName: e.target.value })}
        />
        <button onClick={handleUpdateShelf}>Update Shelf</button>
      </div>

      <div>
        <h2>Delete Shelf</h2>
        <input
          type="text"
          placeholder="Shelf ID"
          value={shelfId}
          onChange={(e) => setShelfId(e.target.value)}
        />
        <button onClick={handleDeleteShelf}>Delete Shelf</button>
      </div>


      {response && (
        <div>
          <h2>Response</h2>
          <pre>{JSON.stringify(response, null, 2)}</pre>
        </div>
      )}
    </div>
  );
}

export default ApiTestPage;
