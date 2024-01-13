// FormComment.js
import React, { useState } from "react";
import { postComments } from "../../Services/CommentService.js";
import RatingStars from "./RatingStars";

function Comment({ productId }) {
  const [commentText, setCommentText] = useState("");
  const [rating, setRating] = useState(0);

  const handleRatingChange = (newRating) => {
    setRating(newRating);
  };
  const handleSubmit = async (event) => {
    event.preventDefault();

    const commentData = {
      productId,
      commentText,
      rating,
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
      <RatingStars onRatingChange={handleRatingChange}/>
      <button type="submit">Submit Comment</button>
    </form>
  );
}

export default Comment;
