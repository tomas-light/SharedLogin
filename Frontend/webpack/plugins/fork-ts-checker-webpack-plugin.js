import Plugin from "fork-ts-checker-webpack-plugin";

export function forkTsCheckerWebpackPlugin() {
    return {
        plugins: [
            new Plugin()
        ]
    };
};