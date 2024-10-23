import React, { useState } from 'react';
import '../css/CustomerInformationPaige.css';
import { getAllUsers } from '../Services/APIs-User.js';
import { getUserByID } from '../Services/APIs-User.js';
import { updateUser } from '../Services/APIs-User.js';

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

    // Check if there is at least one address
    const addressToSend = editingUser.addresses && editingUser.addresses.length > 0 ? editingUser.addresses[0] : null;

    if (!addressToSend) {
      setError("Address is required.");
      return;
    }

    const userToUpdate = {
      ...editingUser,
      Address: addressToSend, // Send the first address as the 'Address' field
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

  return (
  <>
    <div className = "paige-container">
      <div className="Header-Section-CustomerInformationPaige">
        <h1>Customer Information</h1>
      </div>

      <div className="GetAllUsers-Button">
        <button onClick={fetchAllUsers}>Load Customer Information</button>
        {loading && <div>Loading customer information...</div>}
        {error && <div>Error: {error}</div>}
      </div>

      <div className="GetCustomerByID-SearchFunction">
        <input 
          className = "GetUserByID-Input"
          placeholder="Write ID of Customer"
          value={userID}
          onChange={(e) =>setUserId(e.target.value)}
        />
        <button onClick={fetchUserByID}>Get User By ID</button>
      </div>

       {editingUser && (
          <div className="UpdateUser-Button">
            <button onClick={handleUpdateUser}>Update User</button>
          </div>
        )}

      {!loading && !error && (
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
                  <td className="Table-Datacell-Gray">
                      {editingUser && editingUser.id === user.id ? (
                        <input 
                          value={editingUser.userName}
                          onChange={(e) => handleInputChange(e, 'userName')}
                        />
                      ) : (
                        user.userName
                      )}
                    </td>
                  <td className="Table-Datacell-White">
                      {editingUser && editingUser.id === user.id ? (
                        <input 
                          value={editingUser.firstName}
                          onChange={(e) => handleInputChange(e, 'firstName')}
                        />
                      ) : (
                        user.firstName
                      )}
                    </td>
                    <td className="Table-Datacell-Gray">
                      {editingUser && editingUser.id === user.id ? (
                        <input 
                          value={editingUser.lastName}
                          onChange={(e) => handleInputChange(e, 'lastName')}
                        />
                      ) : (
                        user.lastName
                      )}
                    </td>
                    <td className="Table-Datacell-White">
                      {editingUser && editingUser.id === user.id ? (
                        <input 
                          value={editingUser.email}
                          onChange={(e) => handleInputChange(e, 'email')}
                        />
                      ) : (
                        user.email
                      )}
                    </td>
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