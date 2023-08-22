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
    plugins: [function ({ addBase }) {
        addBase({
            'ul': {
                listStyle: 'revert',
            },
            'ol': {
                listStyle: 'revert',
            },
            'menu': {
                listStyle: 'revert',
            }
        })
    }],
};
