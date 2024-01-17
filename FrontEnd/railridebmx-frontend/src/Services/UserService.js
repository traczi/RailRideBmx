const API_BASE_URL = 'https://localhost:7139/RailRideBmx/User';

export const getUser = async (token) => {
    try {
        const response = await fetch(`${API_BASE_URL}/GetUserById`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
        });
        if (!response.ok){
            throw new Error(`HTTP error! status: ${response.status}`)
        }
        return await response.json();
    } catch (error) {
        console.error("Erreur lors de la récupération des informations de l'utilisateur:", error);
        throw error;
    }
};

export const modifyName = async (newName, token) => {
    try {
        const response = await fetch(`${API_BASE_URL}/ModifyName`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify({ ...newName }),
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.ok;
    } catch (error) {
        console.error("Erreur lors de la modification du nom de l'utilisateur:", error);
        throw error;
    }
};

export const modifyEmail  = async (newEmail, token) => {
    try {
        const response = await fetch(`${API_BASE_URL}/ModifyEmail`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(newEmail),
        });
        console.log(response)
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.ok;
    } catch (error) {
        console.error("Erreur lors de la modification du nom de l'utilisateur:", error);
        throw error;
    }
};

export const modifyPassword  = async (oldPassword, newPassword, token) => {
    try {
        const response = await fetch(`${API_BASE_URL}/ChangePassword`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify({ currentPassword: oldPassword, newPassword: newPassword }),
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.ok;
    } catch (error) {
        console.error("Erreur lors de la modification du nom de l'utilisateur:", error);
        throw error;
    }
};