document.addEventListener("DOMContentLoaded", () => {
    // 1. Hide button "Authorize"
    const style = document.createElement("style");
    style.innerHTML = `
    .swagger-ui .authorize {
      display: none !important;
    }
  `;
    document.head.appendChild(style);

    // 2. Wait for Swagger to render the topbar
    const interval = setInterval(() => {
        const topBar = document.querySelector(".topbar-wrapper");
        const swaggerReady = window.ui;

        if (topBar && swaggerReady) {
            clearInterval(interval);

            // 3. Creating a login form
            const wrapper = document.createElement("div");
            wrapper.className = "swagger-login-wrapper";
            wrapper.style = "padding: 10px; display: flex; gap: 10px; align-items: center";

            const emailInput = document.createElement("input");
            emailInput.type = "email";
            emailInput.placeholder = "Email";
            emailInput.style = "padding: 4px; border: 1px solid #ccc; border-radius: 4px";

            const passwordInput = document.createElement("input");
            passwordInput.type = "password";
            passwordInput.placeholder = "Password";
            passwordInput.style = "padding: 4px; border: 1px solid #ccc; border-radius: 4px";

            const loginBtn = document.createElement("button");
            loginBtn.innerText = "🔐 Login";
            loginBtn.style = "padding: 6px 10px; border-radius: 4px; background-color: #61affe; color: white; border: none; cursor: pointer";

            loginBtn.onclick = async () => {
                const email = emailInput.value.trim();
                const password = passwordInput.value;

                if (!email || !password) {
                    alert("Please enter email and password.");
                    return;
                }

                try {
                    const response = await fetch("https://localhost:5041/api/v1/Auth/Login", {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify({ email, password })
                    });

                    if (!response.ok) {
                        alert("❌ Login failed. Check your credentials.");
                        return;
                    }

                    const json = await response.json();
                    const token = json.token || json.value || json;

                    window.ui.preauthorizeApiKey("Bearer", token);
                    alert("✅ Logged in successfully!");
                } catch (error) {
                    console.error("Login error:", error);
                    alert("❌ Login error. See console for details.");
                }
            };

            wrapper.appendChild(emailInput);
            wrapper.appendChild(passwordInput);
            wrapper.appendChild(loginBtn);

            topBar.appendChild(wrapper);
        }
    }, 300);
});
