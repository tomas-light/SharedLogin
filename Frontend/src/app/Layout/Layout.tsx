import * as React from "react";
import { NavBarContainer } from "./NavBar/NavBarContainer";
import { Callback } from "@utils/types/Callback";

export interface ILayoutOwnProps {
    children: any;
}

export interface ILayoutCallProps {
    load: Callback;
}

type Props = ILayoutOwnProps & ILayoutCallProps;

export class Layout extends React.Component<Props> {
    public componentDidMount(): void {
        this.props.load();
    }

    public render() {
        const { children } = this.props;
        return (
            <>
                <NavBarContainer />
                <div>{children}</div>
            </>
        );
    }
}
