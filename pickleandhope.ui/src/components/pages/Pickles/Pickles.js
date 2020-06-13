import React from 'react';
import SinglePickle from '../../shared/SinglePickle/SinglePickle';
import  { getPickles }  from '../../../helpers/data/pickleRequests';

class Pickle extends React.Component {

  state = {
    pickles: []
  };

  // state = {
  //   pickles: [
  //     {id:1, type:'dill', numberInStock:12, size:'large', price:5}
  //   ]
  // };

  componentDidMount() {
    getPickles()
      .then(pickles => this.setState({pickles:pickles}))
      .catch('Trouble getting your pickles')
  }

  render() {
    const { pickles } = this.state;
    return pickles.map(pickle =>
      <SinglePickle key={pickle.id} pickle={pickle}/>);
  }
}

export default Pickle;