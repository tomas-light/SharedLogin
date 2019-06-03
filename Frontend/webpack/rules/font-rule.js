export function fontRule() {
    return {
        module: {
            rules: [
                {
                    test: /\.(png|woff|woff2|eot|ttf|svg)$/,
                    loader: "url-loader?limit=100000"
                },
            ],
        },
    };
};