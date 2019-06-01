import React from "react";

import { makeStyles, Theme, createStyles } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper/Paper";
import Grid from "@material-ui/core/Grid/Grid";
import Typography from "@material-ui/core/Typography/Typography";
import TextField from "@material-ui/core/TextField";
import { Button } from "@material-ui/core";
import { Callback } from "@utils/types/Callback";

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

export const inputIds = {
    email: "login-email",
    password: "login-password"
};

export interface ILoginPageProps {}

export interface ILoginPageCallProps {
    onSubmit: Callback;
}

type Props = ILoginPageProps & ILoginPageCallProps;

const LoginPage: React.FunctionComponent<Props> = props => {
    const { onSubmit } = props;

    const classes = useStyles();

    return (
        <Grid container justify={"center"} className={classes.root}>
            <Paper elevation={2} className={classes.paper}>
                <Grid container direction={"column"}>
                    <Grid item>
                        <Typography
                            variant={"h5"}
                            className={classes.signInText}
                        >
                            Sign in
                        </Typography>

                        <TextField
                            required
                            id={inputIds.email}
                            label="Email"
                            className={classes.textField}
                            margin="normal"
                            variant="outlined"
                            fullWidth
                        />

                        <TextField
                            required
                            id={inputIds.password}
                            label="Password"
                            type={"password"}
                            className={classes.textField}
                            margin="normal"
                            variant="outlined"
                            fullWidth
                        />

                        <Button
                            variant="contained"
                            color="primary"
                            className={classes.loginButton}
                            onClick={onSubmit}
                        >
                            Login
                        </Button>
                    </Grid>
                </Grid>
            </Paper>
        </Grid>
    );
};

const componentWithStyles = LoginPage;
export { componentWithStyles as LoginPage };
