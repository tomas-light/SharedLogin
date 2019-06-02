import makeStyles from "@material-ui/core/styles/makeStyles";
import { Theme } from "@material-ui/core/styles";
import createStyles from "@material-ui/core/styles/createStyles";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            backgroundColor: "#F4F4F4",
            height: "100%",
            width: "100%"
        },
        paper: {
            margin: "auto",
            padding: 16,
            width: 300
        },
        signInText: {},
        textField: {},
        loginButton: {
            marginTop: 16,
            width: "100%"
        }
    })
);

export { useStyles as useLoginPageStyles };
