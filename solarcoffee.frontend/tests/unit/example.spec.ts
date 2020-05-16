import { shallowMount } from "@vue/test-utils";
import HelloWorld from "@/components/HelloWorld.vue";

describe("HelloWorld.vue", () => {
  it("renders props.msg when passed", () => {
    // arrange
    const msg = "new message";

    // act
    const wrapper = shallowMount(HelloWorld, {
      propsData: { msg }
    });

    // assert
    expect(wrapper.text()).toMatch(msg);
  });
});
