import { RouterState } from "connected-react-router";

import { LayoutStore } from "@app/Layout/redux/LayoutStore";
import { SessionStore } from "./SessionStore/SessionStore";

export { createReducers } from "./createReducers";

export class Reducers {
  router: RouterState;
  session: SessionStore;
  layoutStore: LayoutStore;
}
