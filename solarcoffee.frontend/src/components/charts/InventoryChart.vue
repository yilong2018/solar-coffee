<template>
  <div v-if="isTimelineBuilt">
    <apexchart 
      type="area"
      :width="'100%'" 
      height="300" 
      :options="options"
      :series="series"
    ></apexchart>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { IInventoryTimeline } from "../../types/InventoryGraph";
import { Sync, Get } from "vuex-pathify";

import moment from 'moment';

import VueApexCharts from 'vue-apexcharts';
Vue.component('apexchart', VueApexCharts)

@Component({
  name: 'InventoryChart',
  components: {}
})
export default class InventoryChart extends Vue {
  @Sync("snapshotTimeline")
  snapshotTimeline: IInventoryTimeline;
  
  @Get("isTimelineBuilt")
  isTimelineBuilt?: boolean;

  
  
  get options() {
    // console.log(this.snapshotTimeline.timeline);
    // console.log(this.snapshotTimeline.timeline.map(t => moment(t).format("MM-DD HH:mm:ss")));
    return {
      dataLabels: { enabled: false },
      fill: {
        type: "gradient"
      },
      stroke: {
        curve: "smooth"
      },
      xaxis: {
        // categories: this.snapshotTimeline.timeline.map(t => moment(t).add(8, 'hour')),
        categories: this.snapshotTimeline.timeline.map(t => moment(t).add(8,'hours').format("MM-DD HH:mm:ss")),
        type: "datetime"
      }
    };
  }

  get series() {
    // console.log(
    //   this.snapshotTimeline.productInventorySnapshots.map(snapshot => ({
    //   name: snapshot.productId,
    //   data: snapshot.quantityOnHand
    // }))
    // );
    return this.snapshotTimeline.productInventorySnapshots.map(snapshot => ({
      name: snapshot.productId,
      data: snapshot.quantityOnHand
    }));
  }
  
  async created() {
    // await this.$store.dispatch("assignSnapshots");
  }
}
</script>

<style scoped lang="scss">

</style>