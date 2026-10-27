// QuizMaster - Global Site Scripts

document.addEventListener('DOMContentLoaded', () => {
  // 1. Toast notifications auto-dismiss
  const toasts = document.querySelectorAll('.toast-alert');
  toasts.forEach(toast => {
    setTimeout(() => {
      toast.style.transition = 'opacity 0.4s ease, transform 0.4s ease';
      toast.style.opacity = '0';
      toast.style.transform = 'translateX(50px)';
      setTimeout(() => toast.remove(), 400);
    }, 4500);
  });

  // 2. Password Show / Hide toggle
  const togglePasswordBtns = document.querySelectorAll('.btn-toggle-password');
  togglePasswordBtns.forEach(btn => {
    btn.addEventListener('click', () => {
      const targetId = btn.getAttribute('data-target');
      const input = document.getElementById(targetId);
      if (input) {
        if (input.type === 'password') {
          input.type = 'text';
          btn.innerHTML = '<i class="bi bi-eye-slash"></i>';
        } else {
          input.type = 'password';
          btn.innerHTML = '<i class="bi bi-eye"></i>';
        }
      }
    });
  });

  // 3. Quick Demo Login Credentials buttons
  const demoStudentBtn = document.getElementById('btnDemoStudent');
  if (demoStudentBtn) {
    demoStudentBtn.addEventListener('click', () => {
      const emailInput = document.getElementById('loginEmail');
      const passwordInput = document.getElementById('loginPassword');
      if (emailInput && passwordInput) {
        emailInput.value = 'student@quizmaster.com';
        passwordInput.value = 'student123';
      }
    });
  }

  const demoAdminBtn = document.getElementById('btnDemoAdmin');
  if (demoAdminBtn) {
    demoAdminBtn.addEventListener('click', () => {
      const emailInput = document.getElementById('loginEmail');
      const passwordInput = document.getElementById('loginPassword');
      if (emailInput && passwordInput) {
        emailInput.value = 'admin@quizmaster.com';
        passwordInput.value = 'admin123';
      }
    });
  }
});
