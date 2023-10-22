import CalloutView from './screens/shared/views/CalloutView';
import React from 'react';
import ReactDOM from 'react-dom';
import IndicatorView from './screens/shared/views/IndicatorView';
import Main from './screens/main/controllers/Main';
import reportWebVitals from './reportWebVitals';
import 'jquery/src/jquery'
import 'bootstrap/dist/css/bootstrap.css';
import '@fortawesome/fontawesome-free/css/regular.css';
import '@fortawesome/fontawesome-free/css/all.css';
import './presentation/styles/AdminLTE.css';
import './presentation/styles/AdminSkins.css';
import './presentation/styles/App.css';
import 'bootstrap/dist/js/bootstrap.js'

let calloutViewRef = React.createRef<CalloutView>();
const calloutView = (<CalloutView ref={calloutViewRef} />);
ReactDOM.render(calloutView, document.getElementById('calloutWrapper'));

let indicatorViewRef = React.createRef<IndicatorView>();
const indicatorView = (<IndicatorView ref={indicatorViewRef} />);
ReactDOM.render(indicatorView, document.getElementById('indicatorWrapper'));

const mainView = (<Main apiURL={process.env.REACT_APP_API_URL}
  authorization={process.env.REACT_APP_AUTHORIZATION}
  clientID={process.env.REACT_APP_BACKOFFICE_CLIENI_ID}
  clientSecret={process.env.REACT_APP_BACKOFFICE_CLIENI_SECRET} 
  calloutView={calloutViewRef}
  indicatorView={indicatorViewRef} />);

ReactDOM.render(mainView, document.getElementById('pagecontent'));

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
