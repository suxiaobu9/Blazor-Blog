/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["**/*.razor", "**/*.cshtml", "**/*.html", "**/*.cshtml"],
    theme: {
        extend: {
            display: {
                'none': 'hidden'
            }
        },
    },
    plugins: [],
};
