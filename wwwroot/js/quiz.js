// QuizMaster - Quiz Examination Engine & Timer

let quizTimerInterval = null;

function initQuizTimer(totalRemainingSeconds, submitFormId) {
  let secondsLeft = Math.max(0, totalRemainingSeconds);
  const timerDisplay = document.getElementById('timerDisplay');
  const timerPill = document.getElementById('timerPill');
  const quizForm = document.getElementById(submitFormId);

  function updateDisplay() {
    if (secondsLeft <= 0) {
      if (timerDisplay) timerDisplay.textContent = '00:00';
      if (quizTimerInterval) clearInterval(quizTimerInterval);

      // Auto submit
      if (timerPill) {
        timerPill.classList.remove('timer-warning');
        timerPill.classList.add('timer-danger');
      }

      alert('Time is up! Your quiz will now be submitted automatically.');
      if (quizForm) quizForm.submit();
      return;
    }

    const minutes = Math.floor(secondsLeft / 60);
    const seconds = secondsLeft % 60;
    const formatted = `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`;

    if (timerDisplay) {
      timerDisplay.textContent = formatted;
    }

    // Color indicators
    if (timerPill) {
      if (secondsLeft <= 60) {
        timerPill.classList.remove('timer-warning');
        timerPill.classList.add('timer-danger');
      } else if (secondsLeft <= 120) {
        timerPill.classList.add('timer-warning');
      }
    }

    secondsLeft--;
  }

  updateDisplay();
  quizTimerInterval = setInterval(updateDisplay, 1000);
}

// Option selection & auto-save via AJAX
function setupOptionSelection(quizId, questionId) {
  const optionLabels = document.querySelectorAll('.option-label');

  optionLabels.forEach(label => {
    label.addEventListener('click', () => {
      // Clear previous selected state
      optionLabels.forEach(l => l.classList.remove('selected'));
      label.classList.add('selected');

      const radio = label.querySelector('input[type="radio"]');
      if (radio) {
        radio.checked = true;
        const selectedValue = radio.value;

        // Async save to server
        fetch('/Quiz/SaveAnswer', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            quizId: parseInt(quizId),
            questionId: parseInt(questionId),
            selectedOption: selectedValue
          })
        })
        .then(res => res.json())
        .then(data => {
          if (data.success) {
            // Update active navigator pill to answered
            const currentPill = document.querySelector(`.nav-pill[data-question-id="${questionId}"]`);
            if (currentPill) {
              currentPill.classList.add('answered');
              currentPill.classList.remove('unanswered');
            }
          }
        })
        .catch(err => console.error('Error saving answer:', err));
      }
    });
  });
}

// Open submit confirmation modal
function prepareSubmitModal(totalQuestions) {
  const answeredPills = document.querySelectorAll('.nav-pill.answered').length;
  // Also check if current page has an option selected that might not have pill updated yet
  const currentSelected = document.querySelector('.option-label.selected');
  const answeredCountEl = document.getElementById('modalAnsweredCount');
  const unansweredCountEl = document.getElementById('modalUnansweredCount');

  let totalAnswered = answeredPills;
  if (currentSelected) {
    const qId = currentSelected.getAttribute('data-qid');
    const pill = document.querySelector(`.nav-pill[data-question-id="${qId}"]`);
    if (pill && !pill.classList.contains('answered')) {
      totalAnswered++;
    }
  }

  totalAnswered = Math.min(totalQuestions, totalAnswered);
  const unanswered = Math.max(0, totalQuestions - totalAnswered);

  if (answeredCountEl) answeredCountEl.textContent = totalAnswered;
  if (unansweredCountEl) unansweredCountEl.textContent = unanswered;
}
