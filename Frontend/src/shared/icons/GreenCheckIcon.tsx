import * as React from "react";
import SvgIcon, { SvgIconProps } from "@material-ui/core/SvgIcon/SvgIcon";

export const GreenCheckIcon: React.FunctionComponent<SvgIconProps> = props => {
    return (
        <SvgIcon {...props}>
            <g>
                <path
                    d="M8.7203 16.0455L4.4531 11.692L3 13.1641L8.7203 19L21 6.47204L19.5571 5L8.7203 16.0455Z"
                    fill="#008001"
                />
            </g>
        </SvgIcon>
    );
};
