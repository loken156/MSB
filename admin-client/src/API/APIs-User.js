import axios from 'axios';

// Define your API base URL (change this according to your environment)
const API_BASE_URL = 'http://localhost:5000/api';

// Function to fetch all users
export const getAllUsers = async () => {
    try {
        // Make a GET request to the API endpoint
        const response = await axios.get(`${API_BASE_URL}/Getallusers`);

        // Return the users data
        return response.data;
    } catch (error) {
        // Handle errors
        console.error('Error fetching users:', error);
        throw error;
    }
};