import React, { useState } from 'react';
import '../css/CustomerInformationPaige.css';
import { getAllUsers } from '../Services/APIs-User.js';
import { getUserByID } from '../Services/APIs-User.js';
import { updateUser } from '../Services/APIs-User.js';
import { deleteUser } from '../Services/APIs-User.js';

function CustomerInformationPaige() {

  const [users, setUsers] = useState([]); 
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null); 
  const [userID, setUserId] = useState(""); 
  const [editingUser, setEditingUser] = useState(null);

  //----------------------Function to fetch all Users---------------------------
  const fetchAllUsers = async () => 
  {
    setLoading(true);
    setError(null);
    setEditingUser(null);

    try 
    {
      const userData = await getAllUsers(); 
      console.log("Fetched user data:", userData); 
      setUsers(userData);
    } 
    catch (error) 
    {
      console.error("Error fetching users:", error.message);
      setError(error.message);
    } 
    finally 
    {
      setLoading(false);
    }
  };

  //-------------------Function to fetch one User by their ID------------------------
  const fetchUserByID = async () =>
  {
    setLoading(true);
    setError(null);

    console.log("Fetching user by ID:", userID);

    try
    {
      const userData = await getUserByID(userID);
      console.log("Fetched user data:", userData);
      setUsers([userData]);
      setEditingUser(userData);
    }
    catch (error)
    {
      console.error("Error fetching user by ID:", error.message);
      setError(error.message);
    }
    finally
    {
      setLoading(false);
    }
  }

 //----------------------Function to Update User------------------------------
  const handleUpdateUser = async () => {
    if (!editingUser) return;

    const confirmUpdate = window.confirm("Are you sure you want to update this user?");
    if (!confirmUpdate) return; // Exit if the user cancels

    const addressToSend = editingUser.addresses && editingUser.addresses.length > 0 ? editingUser.addresses[0] : null;

    if (!addressToSend) {
      setError("Address is required.");
      return;
    }

    const userToUpdate = {
      ...editingUser,
      Address: addressToSend,
    };

    setLoading(true);
    setError(null);

    try {
      const updatedUser = await updateUser(userToUpdate);
      setUsers(users.map(user => (user.id === updatedUser.id ? updatedUser : user)));
      setEditingUser(null);
      alert('User updated successfully!');
    } catch (error) {
      setError(error.message);
    } finally {
      setLoading(false);
    }
  };

  //----------------------Handle input change in the editable fields------------------------------
  const handleInputChange = (e, field) => 
  {
    const { value } = e.target;
    setEditingUser({ ...editingUser, [field]: value });
  };

  //-------------------Function to Delete User------------------------------
const handleDeleteUser = async () => {
  if (!editingUser) return;

  const confirmDelete = window.confirm("Are you sure you want to delete this user?");
  if (!confirmDelete) return; // Exit if the user cancels

  setLoading(true);
  setError(null);

  try {

    const response = await deleteUser(editingUser);
    console.log('User deleted:', response);

    setUsers(users.filter(user => user.id !== editingUser.id));
    setEditingUser(null);
    alert('User deleted successfully!');
  } catch (error) {
    setError(error.message);
  } finally {
    setLoading(false);
  }
};

//----------------------Handle input change for address fields----------------------
const handleAddressChange = (e, index, fieldName) => {
  const { value } = e.target;
  const updatedAddresses = [...editingUser.addresses];
  updatedAddresses[index] = {
    ...updatedAddresses[index],
    [fieldName]: value,
  };
  setEditingUser((prevUser) => ({
    ...prevUser,
    addresses: updatedAddresses,
  }));
};

  return (
  <>
    <div className = "paige-container">

      <div className="Header-Section-CustomerInformationPaige">
        <h1>Customer Information</h1>
      </div>

      <div className = "search-Update-Delete-Section">

        <div className="GetAllUsers-Button-Section">
          <button className = "GetAllUsers-Button"onClick={fetchAllUsers}>Load Customer Information</button>
          {loading && <div>Loading customer information...</div>}
          {error && <div>Error: {error}</div>}
        </div>

        <div className="GetCustomerByID-SearchFunction-Section">
          <input 
            className = "GetUserByID-Input"
            placeholder="Write ID of Customer"
            value={userID}
            onChange={(e) =>setUserId(e.target.value)}
          />
          <button 
            className = "getUserByIdButton"
            onClick={fetchUserByID}
          >
            Get User By ID
          </button>
        </div>

        <div className="updateAndDelete-Section">
          <button 
            className="updateUserButton" 
            onClick={handleUpdateUser} 
            disabled={!editingUser}
          >
            Update User
          </button>
          <button 
            className="deleteUserButton" 
            onClick={handleDeleteUser} 
            disabled={!editingUser}
          >
            Delete User
          </button>
        </div>

      </div>

      {/* Show specific User Section*/}
      {editingUser ? (
        <div className = "editUser-Info-Section">

          <h2>Customer Details</h2>
          <table className = "editUser-Info-Table">
            <thead>
              <tr>
                <th>ID</th>
                <th>User Name</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Email</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td className = "editUserTable-Datacell-White">{editingUser.id}</td>
                <td className = "editUserTable-Datacell-Gray">
                  <input 
                    className = "editUser-Inputfields"
                    value={editingUser.userName}
                    onChange={(e) => handleInputChange(e, 'userName')}
                  />
                </td>
                <td className = "editUserTable-Datacell-White">
                  <input 
                    className = "editUser-Inputfields"
                    value={editingUser.firstName}
                    onChange={(e) => handleInputChange(e, 'firstName')}
                  />
                </td>
                <td className = "editUserTable-Datacell-Gray">
                  <input 
                    className = "editUser-Inputfields"
                    value={editingUser.lastName}
                    onChange={(e) => handleInputChange(e, 'lastName')}
                  />
                </td>
                <td className = "editUserTable-Datacell-White">
                  <input 
                    className = "editUser-Inputfields"
                    value={editingUser.email}
                    onChange={(e) => handleInputChange(e, 'email')}
                  />
                </td>
              </tr>
            </tbody>
          </table>

          <h3>Addresses</h3>
          <div className="editUser-Address-Section">
            <table className="editUser-Info-Table">
              <thead>
                <tr>
                  <th>Street Name</th>
                  <th>City</th>
                  <th>Zip Code</th>
                  <th>Full Address</th>
                  <th>Unit Number</th>
                </tr>
              </thead>
              <tbody>
                {editingUser.addresses && editingUser.addresses.length > 0 ? (
                  editingUser.addresses.map((address, index) => (
                    <tr key={index}>
                      <td className = "editUserTable-Datacell-White">
                        <input
                          className = "editUser-Inputfields"
                          value = {address.streetName}
                          onChange={(e) => handleAddressChange(e, index, 'streetName')}
                        />
                      </td>
                      <td className = "editUserTable-Datacell-Gray">
                        <input
                          className = "editUser-Inputfields"
                          value = {address.city}
                          onChange = {(e) => handleAddressChange(e, index, 'city')}
                        />
                      </td>
                      <td className = "editUserTable-Datacell-White">
                        <input
                          className = "editUser-Inputfields"
                          value = {address.zipCode}
                          onChange = {(e) => handleAddressChange(e, index, 'zipCode')}
                        />
                      </td>
                      <td className = "editUserTable-Datacell-Gray">
                        {address.fullAddress}
                      </td>
                      <td className = "editUserTable-Datacell-White">
                        <input
                          className = "editUser-Inputfields"
                          value = {address.unitNumber}
                          onChange = {(e) => handleAddressChange(e, index, 'unitNumber')}
                        />
                      </td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td className = "editUserTable-Datacell-Red" colSpan="5">
                      No addresses available.
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>

          <h3>Orders</h3>
          <div className = "editUser-Order-sSection">
            <table className = "editUser-Info-Table">
              <thead>
                <tr>
                  <th>Order Number</th>
                  <th>Date</th>
                  <th>Status</th>
                </tr>
              </thead>
              <tbody>
                {editingUser.orders && editingUser.orders.length > 0 ? (
                  editingUser.orders.map((order, index) => (
                    <tr key = {index}>
                      <td>{order.orderNumber}</td>
                      <td>{new Date(order.orderDate).toLocaleDateString()}</td>
                      <td>{order.orderStatus}</td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td  className = "editUserTable-Datacell-Red"colSpan="3">No orders available.</td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
        
      ) : (

        /* Show all Users Section*/
        <div className="Customer-Info-Section">
          <h2>Customer List</h2>
          <table className="Customer-Info-Table">
            <thead>
              <tr>
                <th>ID</th>
                <th>User Name</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Email</th>
                <th>Full Adress</th>
                <th>Orders</th>
              </tr>
            </thead>
            <tbody>
              {users.map((user) => (
                <tr key={user.id}>
                  <td className = "Table-Datacell-White">{user.id}</td>
                  <td className = "Table-Datacell-Gray">{user.userName}</td>
                  <td className = "Table-Datacell-White">{user.firstName}</td>
                  <td className = "Table-Datacell-Gray">{user.lastName}</td>
                  <td className = "Table-Datacell-White">{user.email}</td>
                  <td className = "Table-Datacell-Gray">
                    {user.addresses && user.addresses.length > 0 && (
                    user.addresses.map((address, index) => (
                    <div key={index}>
                      {address.fullAddress}
                    </div>
                    ))
                    )}
                  </td>
                  <td className="Table-Datacell-White">
                    {user.orders && user.orders.length > 0 ? (
                      <ul>
                        {user.orders.map((order, index) => (
                          <li key={index}>
                            Order Number: {order.orderNumber},
                            Date: {new Date(order.orderDate).toLocaleDateString()},
                            Status: {order.orderStatus}
                          </li>
                        ))}
                      </ul>
                    ) : (
                      "No Orders"
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}

      {!loading && !error && users.length === 0 && (
          <div>No customer information loaded yet.</div>
      )}
    </div>
  </>
  );
}

export default CustomerInformationPaige;