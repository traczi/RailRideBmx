// CommentItem.js
import React, { useState } from 'react';
import {deleteComment, editComment, reportComments} from '../../Services/CommentService';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrash, faPencilAlt, faEllipsisV } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';
Modal.setAppElement('#root');
function CommentItem({ comment, currentUserId, onCommentChange }) {
    const [isEditing, setIsEditing] = useState(false);
    const [editedText, setEditedText] = useState(comment.commentText);
    const [showOptions, setShowOptions] = useState(false);
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const [actionType, setActionType] = useState('');
    const isAuthor = currentUserId === comment.userId;

    const openModal = (action) => {
        setModalIsOpen(true);
        setActionType(action);
    };

    const closeModal = () => {
        setModalIsOpen(false);
    };

    const handleDelete = async () => {
        const token = localStorage.getItem('jwtToken');
        try {
            await deleteComment(comment.id, token);
            onCommentChange();
        } catch (error) {
            console.error("Erreur lors de la suppression du commentaire:", error);
        }
    };

    const handleEdit = async () => {
        if (isEditing) {
            const token = localStorage.getItem('jwtToken');
            try {
                await editComment(comment.id, editedText.toString() , token);
                setIsEditing(false);
                onCommentChange();
            } catch (error) {
                console.error("Erreur lors de la modification du commentaire:", error);
            }
        } else {
            setIsEditing(true);
        }
    };

    const handleReport = async () => {
        try {
            await reportComments(comment.id);
            closeModal();
        } catch (error) {
            console.error("Erreur lors du signalement du commentaire:", error);
        }
    };

    const confirmAction = async () => {
        if (actionType === 'delete') {
            await handleDelete();
        } else if (actionType === 'edit') {
            await handleEdit();
        }else if (actionType === 'signaler') {
            await handleReport();
        }
        closeModal();
    };


    const toggleOptions = () => {
        setShowOptions(!showOptions);
    };

    return (
        <div className="comment-item">
            <p style={{ display: isEditing ? 'none' : 'block' }}>{comment.commentText}</p>
            {isEditing && isAuthor && (
                <textarea value={editedText} onChange={(e) => setEditedText(e.target.value)} />
            )}
            {isAuthor && (
                <div className="comment-options">
                    <button  onClick={toggleOptions} className="options-toggle">
                        <FontAwesomeIcon icon={faEllipsisV} />
                    </button>
                    {showOptions && (
                        <div className="options-menu">
                            <button onClick={() => openModal('edit')} className="edit-button">
                                <FontAwesomeIcon icon={faPencilAlt} />
                                {isEditing ? 'Sauvegarder' : 'Modifier'}
                            </button>
                            {!isEditing && (
                                <button onClick={() => openModal('delete')} className="delete-button">
                                    <FontAwesomeIcon icon={faTrash} />
                                    Supprimer
                                </button>
                            )}
                        </div>

                    )}
                </div>
            )}
            {!isAuthor && (
                <button onClick={() => openModal('signaler')}>Signaler</button>
            )}
            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                contentLabel="Confirmation"
            >
                <h2>Confirmez-vous cette action ?</h2>
                <div className="buttonModal">
                    <button className="confirmButton" onClick={confirmAction}>Oui</button>
                    <button className="cancelButton" onClick={closeModal}>Non</button></div>
            </Modal>
        </div>
    );
}

export default CommentItem;
