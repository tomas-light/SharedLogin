import { connect } from "react-redux";

import { Reducers } from "@reducers";
import { IHistoriesProps, Histories } from "./Histories";

const mapStateToProps = (state: Reducers): IHistoriesProps => {
    return {
        histories: state.historyStore.histories
    };
};
const HistoriesContainer = connect(
    mapStateToProps
)(Histories);

export default HistoriesContainer;
