document.addEventListener('DOMContentLoaded', () => {
    // Smooth scrolling for navigation links
    document.querySelectorAll('nav a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();

            document.querySelector(this.getAttribute('href')).scrollIntoView({
                behavior: 'smooth'
            });

            // If mobile menu is open, close it after clicking a link
            const navUl = document.querySelector('header nav ul');
            if (navUl.classList.contains('show')) {
                navUl.classList.remove('show');
            }
        });
    });

    // Hamburger menu toggle for mobile
    const navToggle = document.createElement('div');
    navToggle.classList.add('hamburger-menu');
    navToggle.innerHTML = '&#9776;'; // Hamburger icon
    document.querySelector('header nav').prepend(navToggle);

    navToggle.addEventListener('click', () => {
        const navUl = document.querySelector('header nav ul');
        navUl.classList.toggle('show');
    });

    // Project image lightbox
    const projectImages = document.querySelectorAll('#projects img');
    const body = document.body;

    projectImages.forEach(img => {
        img.style.cursor = 'pointer'; // Indicate it's clickable
        img.addEventListener('click', function() {
            const lightbox = document.createElement('div');
            lightbox.classList.add('lightbox');
            lightbox.style.display = 'flex'; // Make it visible

            const imgClone = this.cloneNode(); // Clone the clicked image
            imgClone.classList.add('lightbox-content');
            imgClone.removeAttribute('alt'); // Remove alt for lightbox to avoid redundancy

            const closeButton = document.createElement('span');
            closeButton.classList.add('close-button');
            closeButton.innerHTML = '&times;'; // 'x' character

            lightbox.appendChild(imgClone);
            lightbox.appendChild(closeButton);
            body.appendChild(lightbox);

            closeButton.addEventListener('click', () => {
                body.removeChild(lightbox);
            });

            lightbox.addEventListener('click', (e) => {
                if (e.target === lightbox) { // Close if clicked outside image
                    body.removeChild(lightbox);
                }
            });
        });
    });


    // Contact Form Validation
    const contactForm = document.getElementById('contact-form');
    if (contactForm) { // Ensure the form exists before adding listener
        contactForm.addEventListener('submit', function (e) {
            e.preventDefault(); // Prevent default form submission

            const nameInput = document.getElementById('name');
            const emailInput = document.getElementById('email');
            const messageInput = document.getElementById('message');

            let isValid = true;

            // Validate Name
            if (nameInput.value.trim() === '') {
                displayValidationMessage(nameInput, 'Name is required.');
                isValid = false;
            } else {
                removeValidationMessage(nameInput);
            }

            // Validate Email
            if (emailInput.value.trim() === '') {
                displayValidationMessage(emailInput, 'Email is required.');
                isValid = false;
            } else if (!isValidEmail(emailInput.value.trim())) {
                displayValidationMessage(emailInput, 'Please enter a valid email address.');
                isValid = false;
            } else {
                removeValidationMessage(emailInput);
            }

            // Validate Message
            if (messageInput.value.trim() === '') {
                displayValidationMessage(messageInput, 'Message is required.');
                isValid = false;
            } else {
                removeValidationMessage(messageInput);
            }

            if (isValid) {
                alert('Form submitted successfully!'); // Or send data to a server
                this.reset(); // Clear the form
            }
        });
    }

    function isValidEmail(email) {
        // Basic email regex for demonstration
        const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    function displayValidationMessage(element, message) {
        let errorSpan = element.nextElementSibling;
        if (!errorSpan || !errorSpan.classList.contains('error-message')) {
            errorSpan = document.createElement('span');
            errorSpan.classList.add('error-message');
            element.parentNode.insertBefore(errorSpan, element.nextSibling);
        }
        errorSpan.textContent = message;
        errorSpan.style.color = 'red';
        errorSpan.style.fontSize = '0.8em';
        errorSpan.style.marginTop = '5px';
        errorSpan.style.display = 'block';
    }

    function removeValidationMessage(element) {
        let errorSpan = element.nextElementSibling;
        if (errorSpan && errorSpan.classList.contains('error-message')) {
            errorSpan.remove();
        }
    }

    // You could add a simple project filter feature here if needed for step 4,
    // though the prompt didn't fully elaborate on its implementation beyond
    // `filterProjects(category)`.
    // Example (conceptual, not fully implemented for this starter code):
    /*
    function filterProjects(category) {
        const projects = document.querySelectorAll('.project-item');
        projects.forEach(project => {
            if (category === 'all' || project.dataset.category === category) {
                project.style.display = 'block';
            } else {
                project.style.display = 'none';
            }
        });
    }
    // You'd need buttons/links to trigger this:
    // <button onclick="filterProjects('web-dev')">Web Dev</button>
    */
});
