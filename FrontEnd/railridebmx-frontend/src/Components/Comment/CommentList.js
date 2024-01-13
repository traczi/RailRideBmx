// CommentsList.js
import React, { useState, useEffect } from "react";
import { getComments } from "../../Services/CommentService.js";
import {getUserIdFromJWT} from "../Login/Auth";
import CommentItem from "./CommentItem";
import "./Comment.css"

function CommentList({ productId }) {
  const [comments, setComments] = useState([]);
  const currentUserId = getUserIdFromJWT();
  const fetchAndSetComments  = async () => {
    try {
      const commentsDataUser = await getComments(productId);
      setComments(commentsDataUser);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {

    fetchAndSetComments ();
  }, [currentUserId, productId])

  console.log(comments.userId);
  return (
    <div>
      <h3>Comments</h3>
      {comments.length > 0 ? (
        <ul>
          {comments.map((comment, index) => (
            <li key={index}>
                  <CommentItem
                      key={comment.id}
                      comment={comment}
                      currentUserId={currentUserId}
                      onCommentChange={fetchAndSetComments}
                  />
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
