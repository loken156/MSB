
const API_URL = 'https://localhost:5001/api/User';

//-----------------------Fetch all users-----------------------------------------

export const getAllUsers = async () => {

  try 
  {
    const response = await fetch(`${API_URL}/Getallusers`, 
    {
      method: 'GET',
      headers: 
      {
        'accept': '*/*',
      }
    });
    if (!response.ok) 
    {
      throw new Error(`Error: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    return data;
  } 
  catch (error) 
  {
    console.error("Error fetching users:", error);
    throw error;
  }
};

//-----------------------Fetch one User by ID-----------------------------------------

export const getUserByID = async (ID) => {

  try
  {
    const url = `${API_URL}/GetUserbyId?UserId=${ID}`;
        
    console.log("Fetching user from URL:", url);
            
    const response = await fetch (`${API_URL}/GetUserbyId?UserId=${ID}`,
    {
      method: 'GET',
      headers:
      {
        'accept':  '*/*',
      }
    });
    if (!response.ok)
    {
      throw new Error(`Error: ${response.status} ${response.statusText}`);
      
    }

    const data = await response.json();

    return data;
  }
  catch (error)
  {
    console.error("Error fetching the user by ID:", error);
    throw error;
  }
};

//-----------------------Update User-----------------------------------------

export const updateUser = async (updatedUser) => {

  try 
  {
    const url = `${API_URL}/UpdateUser?updatedUserId=${updatedUser.id}`;

    console.log("Updating user from URL:", url);
    
    const response = await fetch(url, {
      method: 'PUT',
      headers: {
        'accept': '*/*',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(updatedUser)
    });

    if (!response.ok) 
    {
      throw new Error(`Error: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();

    return data;
  } 
  catch (error) {
    console.error("Error updating the user:", error);
    throw error;
  }
};

//-----------------------Delete User-----------------------------------------

export const deleteUser = async (deleteUser) => {

  try 
  {
    const url = `${API_URL}/DeleteUserby${deleteUser.id}`;

    console.log("Deleting user from URL:", url);
    
    const response = await fetch(url, {
      method: 'DELETE',
      headers:
      {
        'accept':  '*/*',
      }
    });

    if (response.status !== 204) 
    {
      const data = await response.json();
      return data;
    }
    
    return { message: 'User deleted successfully.' };
  } 
  catch (error) {
    console.error("Error updating the user:", error);
    throw error;
  }
};