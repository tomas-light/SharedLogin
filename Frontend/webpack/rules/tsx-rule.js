import tsNameof from "ts-nameof";

export function tsxRule() {
    return {
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    exclude: /node-modules/,
                    use: [
                        "babel-loader",
                        {
                            loader: "ts-loader",
                            options: {
                                getCustomTransformers: () => ({ before: [tsNameof]}),
                                transpileOnly: true
                            }
                        }
                    ]
                },
            ],
        },
    };
};