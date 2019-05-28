import path from "path";
import merge from "webpack-merge";

import { tsxRule } from "./rules/tsx-rule";

import { forkTsCheckerWebpackPlugin } from "./plugins/fork-ts-checker-webpack-plugin";

module.exports = merge(
    {
        entry: {
            app: [
                "@babel/polyfill",
                "./src/index.tsx"
            ]
        },
        mode: "development",
        devtool: "eval-source-map",
        output: {
            filename: "[name].js",
            path: path.join(__dirname, "/../../WebApp/wwwroot/js/"),
            publicPath: "/js/"
        },
        resolve: {
            extensions: [".js", ".jsx", ".ts", ".tsx"],
            modules: [
                path.resolve(__dirname + "../"),
                path.resolve(__dirname + "../src/"),
                "node_modules"
            ]
        }
    },
    tsxRule(),
    forkTsCheckerWebpackPlugin()
);