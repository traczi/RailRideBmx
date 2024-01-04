const API_BASE_URL = "https://localhost:7139/RailRideBmx";

export const postComments = async (commentData, token) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Comment/AddComment`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(commentData),
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.ok;
  } catch (error) {
    console.error("Could not post comment:", error);
    throw error;
  }
};
export const getComments = async (productId) => {
  try {
    const response = await fetch(
      `${API_BASE_URL}/Comment/GetComment/${productId}`
    );
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.json();
  } catch (error) {
    throw error;
  }
};
