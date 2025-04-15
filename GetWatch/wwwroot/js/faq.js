const faqItemContainer = document.querySelectorAll('.faq-item-con');
const faqText = document.querySelectorAll('.faq-text');

faqItemContainer.forEach((header)=>{
    header.addEventListener('click', () => {
        const faqItem = header.parentElement;
        const faqContent = faqItem.querySelector('.faq-text');

        faqContent.classList.toggle('active');

        if (faqContent.classList.contains('active')) {
            faqContent.style.maxHeight = faqContent.scrollHeight + 'px';
        }else{
            faqContent.style.maxHeight = 0;
        }


    });
})