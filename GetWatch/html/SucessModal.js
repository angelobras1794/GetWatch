// Fecha o modal após 3 segundos
setTimeout(() => {
  const modal = document.getElementById('modalSucesso');
  if (modal) {
    modal.style.display = 'none';
  }
}, 3000);
