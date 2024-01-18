const API_BASE_URL = "https://localhost:7139/RailRideBmx";

export const postComments = async (commentData, token) => {
    const response = await fetch(`${API_BASE_URL}/Comment/AddComment`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(commentData),
    });

    if (!response.ok) {
      const error = Error(`HTTP error! status: ${response.status}`);
      error.status = response.status;
      throw error;
    }
    return await response.ok;
};

export const reportComments = async (commentId, token) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Comment/ReportComment?commentId=${commentId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
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

export const editComment  = async (commentId, updatedComment, token) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Comment/UpdateComment?commentId=${commentId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(updatedComment),
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

export const deleteComment  = async (commentId, token) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Comment/DeleteComment?commentId=${commentId}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
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
export const fetchAverageRating = async (productId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Comment/AverageRating?productId=${productId}`);
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    return await response.json();

  } catch (error) {
    console.error("Erreur lors de la récupération de la note moyenne:", error);
    throw error;
  }
};
