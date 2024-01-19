// FormComment.js
import React, { useState } from "react";
import { postComments } from "../../Services/CommentService.js";
import RatingStars from "./RatingStars";
import {toast} from "react-toastify";

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
      toast.success("Commentaire créé:", data);
      // Handle success, e.g., clearing the form or giving success feedback
    } catch (error) {
      if (error.status === 400) {
        toast.error("Vous avez déjà commenté ce produit.");
      } else {
        toast.error("Une erreur est survenue lors de l'envoi du commentaire.");
      }
      console.error("Could not post comment:", error);
    }
  };

  return (
    <form className="comment-form" onSubmit={handleSubmit}>
      <div className="comment-submit">
        <label className="comment-title">Commentaire : </label>
          <textarea
              className="comment-textarea"
            value={commentText}
            onChange={(e) => setCommentText(e.target.value)}
            required
          />
      </div>
      <RatingStars onRatingChange={handleRatingChange}/>
      <button className="comment-submitbutton" type="submit">Submit Comment</button>
    </form>
  );
}

export default Comment;
