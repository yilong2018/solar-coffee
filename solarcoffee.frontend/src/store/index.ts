import Vue from "vue";
import Vuex from "vuex";
import pathify from 'vuex-pathify';

import global from '@/store/global-store'

pathify.options.mapping = 'simple';
pathify.options.deep = 2;

Vue.use(Vuex);

export default new Vuex.Store({
  plugins: [pathify.plugin],
  ...global,
  mutations: {},
  actions: {},
  modules: {}
});
