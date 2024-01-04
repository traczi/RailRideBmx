// FormComment.js
import React, { useState } from "react";
import { postComments } from "../../Services/CommentService.js";

function Comment({ productId }) {
  const [commentText, setCommentText] = useState("");
  const [rating, setRating] = useState("");

  const handleSubmit = async (event) => {
    event.preventDefault();

    const commentData = {
      productId,
      commentText,
      rating: parseInt(rating, 10),
    };

    try {
      const token = localStorage.getItem("jwtToken");
      const data = await postComments(commentData, token);
      console.log("Commentaire créé:", data);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Comment Text:
        <textarea
          value={commentText}
          onChange={(e) => setCommentText(e.target.value)}
          required
        />
      </label>
      <label>
        Rating:
        <input
          type="number"
          value={rating}
          onChange={(e) => setRating(e.target.value)}
          min="1"
          max="5"
          required
        />
      </label>
      <button type="submit">Submit Comment</button>
    </form>
  );
}

export default Comment;
