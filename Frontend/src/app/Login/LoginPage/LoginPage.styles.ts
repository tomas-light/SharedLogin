import makeStyles from "@material-ui/core/styles/makeStyles";
import { Theme } from "@material-ui/core/styles";

const styles = makeStyles((theme: Theme) => {
    paper: {
        backgroundColor: theme.palette.secondary;
    }
});

export { styles as LoginPageStyles };
