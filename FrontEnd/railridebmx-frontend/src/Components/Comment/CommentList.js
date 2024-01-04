// CommentsList.js
import React, { useState, useEffect } from "react";
import { getComments } from "../../Services/CommentService.js";

function CommentList({ productId }) {
  const [comments, setComments] = useState([]);

  useEffect(() => {
    const fetchComments = async () => {
      try {
        const commentsData = await getComments(productId);
        setComments(commentsData);
        console.log(productId);
      } catch (error) {
        console.log(error);
      }
    };

    fetchComments();
  }, [productId]);

  return (
    <div>
      <h3>Comments</h3>
      {comments.length > 0 ? (
        <ul>
          {comments.map((comment, index) => (
            <li key={index}>
              <p>{comment.commentText}</p>
              {/* <p>Rating: {comment.rating}</p> */}
            </li>
          ))}
        </ul>
      ) : (
        <p>No comments yet.</p>
      )}
    </div>
  );
}

export default CommentList;
