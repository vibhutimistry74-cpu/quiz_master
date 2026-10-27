// QuizMaster - Admin Portal Scripts

document.addEventListener('DOMContentLoaded', () => {
  // Delete Quiz Modal
  const deleteQuizModal = document.getElementById('deleteQuizModal');
  if (deleteQuizModal) {
    deleteQuizModal.addEventListener('show.bs.modal', (event) => {
      const button = event.relatedTarget;
      const quizId = button.getAttribute('data-quiz-id');
      const quizTitle = button.getAttribute('data-quiz-title');

      const modalTitleEl = document.getElementById('deleteQuizModalTitle');
      const modalIdInput = document.getElementById('deleteQuizIdInput');

      if (modalTitleEl) modalTitleEl.textContent = quizTitle;
      if (modalIdInput) modalIdInput.value = quizId;
    });
  }

  // Delete Question Modal
  const deleteQuestionModal = document.getElementById('deleteQuestionModal');
  if (deleteQuestionModal) {
    deleteQuestionModal.addEventListener('show.bs.modal', (event) => {
      const button = event.relatedTarget;
      const questionId = button.getAttribute('data-question-id');
      const quizId = button.getAttribute('data-quiz-id');
      const questionText = button.getAttribute('data-question-text');

      const modalTextEl = document.getElementById('deleteQuestionModalText');
      const modalIdInput = document.getElementById('deleteQuestionIdInput');
      const modalQuizIdInput = document.getElementById('deleteQuestionQuizIdInput');

      if (modalTextEl) modalTextEl.textContent = questionText;
      if (modalIdInput) modalIdInput.value = questionId;
      if (modalQuizIdInput) modalQuizIdInput.value = quizId;
    });
  }
});
