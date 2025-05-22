
window.menuItensInit = function(){
    var menuItens = document.querySelectorAll('.menu-item');
var infoItens = document.querySelectorAll('.info-item');

menuItens.forEach((header, index) => {
    header.addEventListener('click', () => {
        console.log('clicou');
        menuItens.forEach(item => item.classList.remove('active'));
        infoItens.forEach(item => {
            item.classList.remove('active');
        });

        // Adiciona a classe 'active' ao item clicado e ao conteúdo correspondente
        header.classList.add('active');
        infoItens.forEach((item, i) => {
            if (i === index) {
                item.style.display = 'flex'; // Garante que o item ativo seja exibido
                setTimeout(() => {
                    item.classList.add('active'); // Aplica a animação
                }, 10);
            } else {
                setTimeout(() => {
                    item.style.display = 'none'; // Esconde os itens não ativos
                }, 300); // Tempo deve coincidir com o tempo da transição no CSS
            }
        });
    });
});
}
